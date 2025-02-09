
using Newtonsoft.Json;

internal class Program
{

    class Contact { 
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }

    class ContactBook { 

        private List<Contact> contacts = new List<Contact>();
        private string filePath = "contact.json";

        public ContactBook() {
            LoadContact();
        }

        public void SaveContacts() { 
            string json = JsonConvert.SerializeObject(contacts,Formatting.Indented);
            File.WriteAllText(filePath,json);
        }

        public void LoadContact() {
            if (File.Exists(filePath)) { 
               string json = File.ReadAllText(filePath);
               contacts = JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();
            }
        }

        public void AddContact(string name, string phone, string email) {
            contacts.Add(new Contact{ Name = name,Phone = phone, Email =email});
            SaveContacts();
            Console.WriteLine("Contact added successfully.");
        }

        public void RemoveContact(string name)
        {
            var contact = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contact != null) { 
                contacts.Remove(contact);

                SaveContacts();
                Console.WriteLine("Contact {0} removed successfully", name);
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }

        }

        public void DisplayContacts() {
            if (contacts.Count == 0) {
                Console.WriteLine("No contact available");
                return;
            }
            Console.WriteLine("\nContact List");
            foreach (var contact in contacts) { 
                Console.WriteLine($"Name = {contact.Name}, Phone = {contact.Phone},Email = {contact.Email}");
            }
        }

        public void UpdateContact(string name, string phone, string email)
        {
            var contact = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contact != null)
            {
                contact.Phone = phone;
                contact.Email = email;
                SaveContacts();
                Console.WriteLine($"Contanct {contact.Name} has updated");

            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }

        }

    private static void Main(string[] args)
    {

        ContactBook contactBook = new ContactBook();

        while (true) {

            Console.WriteLine("\nContact Book Application");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Remove Contact");
            Console.WriteLine("3. Display Contacts");
            Console.WriteLine("4. Update Contact");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Phone Number: ");
                    string phone = Console.ReadLine();
                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();
                    contactBook.AddContact(name, phone, email);
                    break;
                case "2":
                    Console.Write("Enter Name of Contact to Remove: ");
                    string nameToRemove = Console.ReadLine();
                    contactBook.RemoveContact(nameToRemove);
                    break;
                case "3":
                    contactBook.DisplayContacts();
                    break;
                case "4":
                    Console.Write("Enter Name of Contact to Update: ");
                    string nameToUpdate = Console.ReadLine();
                    Console.Write("Enter New Phone Number: ");
                    string newPhone = Console.ReadLine();
                    Console.Write("Enter New Email: ");
                    string newEmail = Console.ReadLine();
                    contactBook.UpdateContact(nameToUpdate, newPhone, newEmail);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        }

    }
}