namespace ProductivityApp.Shared.CustomUserSessionExceptions
{
    public  class UserSessionNotFoundException:Exception
    {


        public UserSessionNotFoundException(string message):base(message)
        {

        }
    }
}
