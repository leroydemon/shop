
namespace ConsoleApp1
{
    public class User
    {
        public static string Likes(string[] name)
        {
            string template = " likes this";
            string template1 = " like this";

            for (int i = 0; i < name.Length; i++) 
            {
                if (name.Length == 0)
                {
                    return "no one" + template;
                }
                else if (name.Length == 1)
                {
                    return (name[0] + template);    
                }
                else if (name.Length == 2)
                {
                    return (name[0] + " and " + name[1] + template1);
                }
                else if (name.Length == 3)
                {
                    return (name[0] + ", " + name[1] + " and " + name[2] + template1);
                }
                else
                {
                    return ($"{name[0]}, {name[1]} and {name.Length - 2} others {template1}");
                }
            }
            return "no one" + template;
        }
    }
}

