namespace WebApplication1
{
    public record PersonBaseDto
    {
        public string Position { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
    }
    public record PersonDto : PersonBaseDto
    {
        public int Id { get; set; }
    }

    public record PersonCreateDto : PersonBaseDto
    { }

    public record PersonDeleteDto : PersonDto
    { }

    public record PersonEditDto : PersonDto
    { }
}