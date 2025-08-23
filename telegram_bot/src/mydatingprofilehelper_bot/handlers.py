# -*- coding: utf-8 -*-

import os
from aiogram import types, Bot, Dispatcher, executor
from aiogram.dispatcher.filters import Command
from .config import BACKEND_API_URL, PAYMENT_PROVIDER_TOKEN, BACKEND_API_KEY # Импортируем BACKEND_API_KEY
import requests
import json
import logging
from aiogram.types.message import ContentType
import time
# Настройка логирования
# Уровень логирования INFO покажет информационные сообщения и ошибки
logging.basicConfig(level=logging.INFO)

# Глобальная переменная для хранения данных о пакетах кристаллов
crystal_packages_data = []

# Функция для загрузки данных о пакетах кристаллов из JSON файла
def load_crystal_packages():
    """
    Загружает данные о пакетах кристаллов из crystal_packages.json.
    """
    # Получаем путь к текущей директории скрипта
    script_dir = os.path.dirname(os.path.abspath(__file__))
    json_file_path = os.path.join(script_dir, 'crystal_packages.json')
    try:
        with open(json_file_path, 'r', encoding='utf-8') as f:
            global crystal_packages_data
            crystal_packages_data = json.load(f)
    except FileNotFoundError:
        logging.error(f"Файл конфигурации пакетов кристаллов не найден: {json_file_path}")
    except json.JsonDecodeError:
        logging.error(f"Ошибка парсинга JSON файла конфигурации пакетов кристаллов: {json_file_path}")

# Обработчик команды /start
async def cmd_start(message: types.Message):
    """
    Обрабатывает команду /start.
    Отправляет приветственное сообщение.
    """
    await message.answer("Привет! Я помогу тебе с оплатой кристаллов и расскажу о твоем балансе.")

    telegram_identifier = message.from_user.username if message.from_user.username else str(message.from_user.id)
    logging.info(f"Received /start from user: {telegram_identifier}")

    try:
        # Выполняем запрос к бэкенду для получения профиля пользователя
        # Используем telegramIdentifier для поиска (предполагаем, что бэкенд умеет искать по username или id)
        backend_url = f"{BACKEND_API_URL}/api/user/balance/telegram/{telegram_identifier}"
        logging.info(f"Requesting user balance from backend: {backend_url}")
        response = requests.get(backend_url)

        if response.status_code == 200:
            data = response.json()
            # Ожидаем, что бэкенд вернет объект с полями (например, name, balance, phoneNumber)
            user_name = data.get("name", "Неизвестно")
            user_balance = data.get("balance", 0)
            user_phone = data.get("phoneNumber", "Не указан")

            # Создаем Inline клавиатуру с кнопками
            keyboard = types.InlineKeyboardMarkup()
            keyboard.add(types.InlineKeyboardButton("Пополнить", callback_data="top_up_crystals"))
            keyboard.add(types.InlineKeyboardButton("История транзакций", callback_data="transaction_history"))

            # Выводим краткую информацию о пользователе и клавиатуру
            await message.answer(
                f"Добро пожаловать, {user_name}!\n"
                f"Номер телефона: {user_phone}\n"
                f"Твой текущий баланс кристаллов: {user_balance}\n\n"
                "Что ты хочешь сделать?",
                reply_markup=keyboard
            )

# Обработчик команды /balance
async def cmd_balance(message: types.Message):
    """
    Обрабатывает команду /balance.
    Запрашивает баланс кристаллов пользователя с бэкенда и сообщает его.
    """
    telegram_user_id = message.from_user.id
    telegram_identifier = message.from_user.username if message.from_user.username else str(message.from_user.id)
    logging.info(f"Received /balance from user: {telegram_identifier}")

    try:
        # Выполняем запрос к бэкенду для получения баланса пользователя
        backend_url = f"{BACKEND_API_URL}/api/user/balance/telegram/{telegram_identifier}"
        logging.info(f"Requesting user balance from backend: {backend_url}")
        response = requests.get(backend_url)

        if response.status_code == 200:
            data = response.json()
            # Ожидаем, что бэкенд вернет объект с полем "balance"
            balance = data.get("balance", 0)
            await message.answer(f"Твой текущий баланс кристаллов: {balance}")
        elif response.status_code == 404:
            await message.answer("Твой аккаунт Telegram не привязан к профилю приложения. Пожалуйста, войди в приложение и привяжи свой Telegram.")
        else:
            await message.answer("Не удалось получить информацию о балансе. Попробуй позже.")

    except requests.exceptions.RequestException as e:
        logging.error(f"Ошибка при запросе баланса с бэкенда: {e}")
        await message.answer("Произошла ошибка при получении баланса. Попробуй позже.")
    except Exception as e:
        logging.error(f"Неожиданная ошибка в обработчике баланса: {e}")
        await message.answer("Произошла внутренняя ошибка. Попробуй позже.")

# Обработчик CallbackQuery для кнопок
async def handle_callback_query(call: types.CallbackQuery):
    """
    Обрабатывает нажатия на Inline кнопки.
    """
    # Всегда отвечаем на callback query, даже если просто закрываем уведомление
    await call.answer() 

    # Проверяем данные callback query
    if call.data == "top_up_crystals":
        # Пользователь нажал кнопку "Пополнить"
        logging.info(f"User {call.from_user.id} pressed 'Пополнить' button.")

        # Создаем Inline клавиатуру с пакетами кристаллов
        keyboard = types.InlineKeyboardMarkup(row_width=1) # Можно настроить количество кнопок в ряду
        # Используем данные из загруженного JSON файла
        for package in crystal_packages_data:
            # Создаем кнопку для каждого пакета
            # В callback_data сохраняем информацию о пакете для дальнейшей обработки оплаты
            # Например, используем префикс 'buy_' и количество кристаллов
            button_text = f"{package['amount']} кристаллов за {package['price']} руб."
            callback_data = f"buy_{package['amount']}" 
            keyboard.add(types.InlineKeyboardButton(button_text, callback_data=callback_data))

        # Отправляем сообщение со списком пакетов кристаллов
        await call.message.answer("Выбери пакет кристаллов для пополнения:", reply_markup=keyboard)

    elif call.data.startswith("buy_"):
        # Пользователь выбрал пакет кристаллов для покупки
        try:
            # Извлекаем количество кристаллов из callback_data
            amount_str = call.data.split('_')[1]
            amount = int(amount_str)
            
            # Находим соответствующий пакет в конфигурации
            # Ищем пакет в загруженных данных
            selected_package = next((p for p in crystal_packages_data if p['amount'] == amount), None)

            if selected_package:
                # Формируем параметры для инвойса
                title = f"Пополнение баланса на {selected_package['amount']} кристаллов"
                description = f"Оплата {selected_package['amount']} кристаллов для профиля знакомств"
                # Payload для инвойса, который вернется при успешной оплате.
                # Используем формат buy_amount, чтобы легко извлечь количество кристаллов.
                invoice_payload = f"buy_{selected_package['amount']}"
                currency = "RUB" # Валюта платежа
                # Цена в минимальных единицах (копейках для рублей)
                price_in_kopeks = selected_package['price'] * 100 
                
                # Создаем список объектов LabeledPrice
                prices = [
                    types.LabeledPrice(label=title, amount=price_in_kopeks)
                ]

                # Создаем Inline клавиатуру с кнопкой "Оплатить"
                pay_keyboard = types.InlineKeyboardMarkup()
                pay_keyboard.add(types.InlineKeyboardButton(text="Оплатить", pay=True))
                
                # Отправляем инвойс пользователю
                await call.bot.send_invoice(
                    chat_id=call.message.chat.id,
                    title=title,
                    description=description,
                    payload=invoice_payload,
                    provider_token=PAYMENT_PROVIDER_TOKEN,
                    currency=currency,
                    prices=prices,
                    start_parameter="buy_crystals" # Параметр для deep linking (можно оставить статичным или генерировать)
                    reply_markup=pay_keyboard # Добавляем клавиатуру с кнопкой оплаты
                )                
                logging.info(f"Sent invoice for {amount} crystals to user {call.from_user.id}")

            else:
                await call.message.answer("Извините, выбранный пакет кристаллов недоступен.")
        except (IndexError, ValueError) as e:
            logging.error(f"Failed to process buy callback data '{call.data}': {e}")
            await call.message.answer("Произошла ошибка при обработке вашего запроса. Попробуйте снова.")
    # TODO: Добавить обработку других callback_data, например, для истории транзакций.

# Обработчик Pre-Checkout Query (перед подтверждением оплаты)
async def process_precheckout_query(pre_checkout_query: types.PreCheckoutQuery):
    """
    Обрабатывает Pre-Checkout Query перед подтверждением оплаты пользователем.
    Подтверждает готовность принять платеж.
    """
    logging.info(f"Received PreCheckoutQuery from user {pre_checkout_query.from_user.id} for invoice payload: {pre_checkout_query.invoice_payload}")
    # Отвечаем на Pre-Checkout Query, подтверждая готовность принять платеж
    await pre_checkout_query.answer(ok=True)

# Обработчик Successful Payment (после успешной оплаты)
async def process_successful_payment(message: types.Message):
    """
    Обрабатывает сообщение об успешной оплате.
    Отправляет запрос на бэкенд для зачисления кристаллов с логикой повторных попыток.
    """
    # Получаем информацию об успешном платеже
    successful_payment = message.successful_payment
    logging.info(f"Successful payment received from user {message.from_user.id}.")
    logging.info(f"Payment Info: {successful_payment}")

    # Извлекаем payload платежа, который мы передавали при создании инвойса
    # В payload мы ожидаем строку, например, "buy_100_rub_300" или "buy_100"
    # Нам нужно получить количество кристаллов для зачисления.
    # Предполагаем, что payload содержит amount, например "buy_100"
    try:
        payload_data = successful_payment.invoice_payload.split('_')
        if len(payload_data) > 1 and payload_data[0] == 'buy':
            amount_to_credit = int(payload_data[1])
            logging.info(f"Amount to credit extracted from payload: {amount_to_credit}")
        else:
            raise ValueError("Invalid invoice payload format")
    except (ValueError, IndexError) as e:
        logging.error(f"Failed to parse invoice payload '{successful_payment.invoice_payload}': {e}")
        await message.answer("Произошла ошибка при обработке информации об оплате. Пожалуйста, свяжитесь с поддержкой.")
        return

    # Вызываем бэкенд для пополнения баланса пользователя
    telegram_identifier = message.from_user.username if message.from_user.username else str(message.from_user.id)
    backend_url = f"{BACKEND_API_URL}/api/transaction/credit/telegram" # URL эндпоинта для пополнения
    payload = {
        "telegramIdentifier": telegram_identifier,
        "amount": amount_to_credit
    }

    retries = 5
    for attempt in range(retries):
        try:
            logging.info(f"Attempt {attempt + 1}/{retries}: Sending credit request to backend: {backend_url} with payload: {payload}")
            # Добавить механизм аутентификации бота на бэкенде (например, API ключ в заголовке)
            headers = {
                'X-API-Key': BACKEND_API_KEY
            }
            response = requests.post(backend_url, json=payload)

            if response.status_code == 200: # Предполагаем 200 OK при успешном зачислении
                logging.info("Backend successfully credited crystals.")
                await message.answer(f"🎉 Твой баланс успешно пополнен на {amount_to_credit} кристаллов!")
                return # Успех, выходим из цикла
            else:
                logging.warning(f"Backend returned status code {response.status_code}. Retrying...")
        except requests.exceptions.RequestException as e:
            logging.error(f"Request to backend failed on attempt {attempt + 1}/{retries}: {e}. Retrying...")
        time.sleep(2 ** attempt) # Экспоненциальная задержка перед повторной попыткой

    # Если все попытки исчерпаны и баланс не зачислен
    logging.error(f"Failed to credit crystals for user {message.from_user.id} after {retries} attempts.")
    await message.answer("Произошла ошибка при зачислении кристаллов. Платеж прошел, но баланс не обновлен. Пожалуйста, свяжись с поддержкой.")

# TODO: Зарегистрировать обработчики в Dispatcher в bot.py
