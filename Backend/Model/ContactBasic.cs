namespace BackendRestAPI.Model
{
    public class ContactBasic
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }

        public ContactBasic(Guid id,
                       string name,
                       string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
        public ContactBasic(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Surname = contact.Surname;
        }
    }
}
