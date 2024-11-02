using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace InventoryPharmacy.View
{
    public class TgMessage
    {
        public TgMessage() { }

        public async void SendMessage(string text)
        {
            var token = "7711609097:AAHZ8vnfajQi3psUuqrHhWSWraycn0u-DIE";
            var chatId = "1230312571";
            var botClient = new TelegramBotClient(token);

            try
            {
                await
                    botClient.SendTextMessageAsync(chatId, text, parseMode: ParseMode.MarkdownV2);
                Console.WriteLine("Сообщение отправлено");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
