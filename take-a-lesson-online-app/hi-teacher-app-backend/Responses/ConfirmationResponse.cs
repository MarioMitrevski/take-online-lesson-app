using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hi_teacher_app_backend.Responses
{
    public class ConfirmationResponse
    {
        public string ConfirmationMessage;

        public ConfirmationResponse()
        {
            this.ConfirmationMessage = "Entity succesfully saved";
        }
    }
}
