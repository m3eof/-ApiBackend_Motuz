namespace ApiBackend.Authorization
{
    public class AllowAnonymousAttribute
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class AllowAnonymousAttributeAttribute : Attribute { }
    }
}
