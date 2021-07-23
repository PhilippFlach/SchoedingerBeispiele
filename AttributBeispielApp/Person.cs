namespace AttributBeispielApp
{
    internal class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string GetFullName() => string.Format($"{this.Firstname}, {this.Lastname}");
    }
}