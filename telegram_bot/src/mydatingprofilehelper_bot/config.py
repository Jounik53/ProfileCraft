# -*- coding: utf-8 -*-

import os

# Чтение токена Telegram бота из переменных окружения
# TELEGRAM_BOT_TOKEN - Токен вашего Telegram бота, полученный от BotFather
TELEGRAM_BOT_TOKEN = os.getenv("TELEGRAM_BOT_TOKEN")
if not TELEGRAM_BOT_TOKEN:
 raise ValueError("Переменная окружения TELEGRAM_BOT_TOKEN не установлена")

# Чтение URL базового API бэкенда из переменных окружения
# BACKEND_API_URL - URL базового API вашего бэкенда
BACKEND_API_URL = os.getenv("BACKEND_API_URL")
if not BACKEND_API_URL:
 raise ValueError("Переменная окружения BACKEND_API_URL не установлена")

# Чтение ключа API для аутентификации бота на бэкенде из переменных окружения
# BACKEND_API_KEY - Ключ API для аутентификации бота на бэкенде
BACKEND_API_KEY = os.getenv("BACKEND_API_KEY")
if not BACKEND_API_KEY:
 raise ValueError("Переменная окружения BACKEND_API_KEY не установлена")

# Дополнительные настройки, если нужны
# Например, идентификатор администратора
# ADMIN_CHAT_ID = 123456789

# Чтение токена платежного провайдера из переменных окружения
# PAYMENT_PROVIDER_TOKEN - Токен платежного провайдера (например, YooKassa)
PAYMENT_PROVIDER_TOKEN = os.getenv("PAYMENT_PROVIDER_TOKEN")
if not PAYMENT_PROVIDER_TOKEN:
 raise ValueError("Переменная окружения PAYMENT_PROVIDER_TOKEN не установлена")