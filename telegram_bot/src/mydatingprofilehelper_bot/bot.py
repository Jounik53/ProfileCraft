import logging
from aiogram import Bot, Dispatcher, types
from aiogram.utils import executor
from handlers import router  # Импортируем роутер с хэндлерами

from config import TELEGRAM_BOT_TOKEN

# Включаем логирование, чтобы видеть запросы в консоли
logging.basicConfig(level=logging.INFO)

# Инициализация бота
bot = Bot(token=TELEGRAM_BOT_TOKEN)
dp = Dispatcher(bot)

# Регистрация роутера с хэндлерами
dp.include_router(router)

# Запуск бота
if __name__ == '__main__':
    executor.start_polling(dp, skip_updates=True)