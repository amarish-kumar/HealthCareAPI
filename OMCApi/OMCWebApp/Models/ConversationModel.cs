using OMC.Models;

namespace OMCWebApp.Models
{
    public class ConversationModel
    {
        public Conversation ConversationObject { get; set; }

        public ConversationModel()
        {
            ConversationObject = new Conversation();
        }
    }
}