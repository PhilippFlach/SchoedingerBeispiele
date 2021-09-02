namespace AttributBeispielApp
{
    [Author("Schrödinger")]
    [Author("Bernhard", 1.1)]
    internal class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        [Author("Philipp", 1.2)]
        public string GetFullName() => string.Format($"{this.Firstname}, {this.Lastname}");
    }
}