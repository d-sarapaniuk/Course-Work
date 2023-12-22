using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelReservationService.Service;

namespace HotelReservationService.Presentation
{
    public class ConsoleMenu
    {
        BookingService bookingService = new BookingService();
        CustomerService customerService = new CustomerService();
        HotelService hotelService = new HotelService();
        RoomService roomService = new RoomService();
        private int selectedOption = 0;
        private string[] mainOptions =
        {
            "Booking Service",
            "Hotel Service",
            "Customer Service",
            "Search"

        };
        private string[] bookingOptions =
        {
            "Book a room",
            //"Cancel a booking"
        };
        private string[] hotelOptions =
        {
            "List of Hotels",
            "Add a hotel",
            "Remove a hotel"
        };
        private string[] customerOptions =
        {
            "Customers sorted by last name",
            "Customers sorted by first name",
            "Add a customer",
            //"Edit a customer",
            "Remove a customer"
        };
        private string[] hotelinfoOptions =
        {
            "Add a room",
            "See available rooms",
            "See reserved rooms",
            "Return to menu"

        };

        private delegate void Delegate();
        Delegate menuDelegate;

        public ConsoleMenu()
        {
            MainMenu();
        }
        public void MainMenu()
        {
            menuDelegate = MainMenu;
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(mainOptions, "[ENTER] to select a service:");
            switch (selected)
            {
                case 0: BookingMenu(); break;
                case 1: HotelsMenu(); break;
                case 2: CustomerMenu(); break;
                case 3: Search(); break;
            }
        }
        private void Search()
        {
            Console.Clear();
            Console.Write("Enter a key word:");
            string input = Console.ReadLine();
            var list = customerService.Search(input);
            Console.WriteLine("Resuts:");
            foreach ( var item in list ) { Console.WriteLine(item); }
            returnESC();

        }
        private void BookingMenu()
        {
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(bookingOptions);
            switch (selected)
            {
                case 0: BookARoom(); break;
                //case 1: CancelBooking(); break;
            }
        }
        private void HotelsMenu()
        {
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(hotelOptions);
            switch (selected)
            {
                case 0: ShowHotels(); break;
                case 1: AddHotel(); break;
                case 2: RemoveHotel(); break;
            }
        }
        private void CustomerMenu()
        {
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(customerOptions);
            switch (selected)
            {
                case 0: CustomersSortLastName(); break;
                case 1: CustomerSortFirstName(); break;
                case 2: AddCustomer(); break;
                //case 3: EditCustomer(); break;
                case 3: RemoveCustomer(); break;
            }
        }

        private void CustomersSortLastName()
        {
            Console.Clear();
            Console.WriteLine("Customers sorted by last name:");
            Dictionary<int, string> dict = customerService.getCustomers(CustomerService.SortingOptions.ByLastName);
            foreach(string customer in dict.Values)
            {
                Console.WriteLine(customer);
            }
            returnESC();

        }
        private void CustomerSortFirstName()
        {
            Console.Clear();
            Console.WriteLine("Customers sorted by last name:");
            Dictionary<int, string> dict = customerService.getCustomers(CustomerService.SortingOptions.ByFirstName);
            foreach (string customer in dict.Values)
            {
                Console.WriteLine(customer);
            }
            returnESC();
        }
        private void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("Adding a customer to a database!");
            string firstName = getFromUser("first name");
            string lastName = getFromUser("last name");
            string phone = getFromUser("phone number");
            customerService.AddCustomer(firstName, lastName, phone);
            Console.WriteLine("A new customer was added!");
            returnESC();
        }
        private void RemoveCustomer()
        {
            selectedOption = 0;
            Console.Clear();
            if (customerService.getCustomers().Count == 0)
            {
                Console.WriteLine("There are no customers in a database!");
                returnESC();
            }
            string[] customers = new string[customerService.getCustomers().Count];
            int[] customerIds = new int[customerService.getCustomers().Count];
            customerService.getCustomers(CustomerService.SortingOptions.ByLastName).Values.CopyTo(customers, 0);
            customerService.getCustomers(CustomerService.SortingOptions.ByLastName).Keys.CopyTo(customerIds, 0);
            
            int selected = getSelectedOption(customers, "Select a customer to remove:");
            customerService.RemoveCustomer(customerIds[selected]);
            returnESC();
        }
        private void BookARoom()
        {
            selectedOption = 0;
            Console.Clear();
            if (customerService.getCustomers().Count == 0)
            {
                Console.WriteLine("There are no customers that can book a room!");
                returnESC();
            }

            string[] customers = new string[customerService.getCustomers().Count];
            int[] customerIds = new int[customerService.getCustomers().Count];
            customerService.getCustomers(CustomerService.SortingOptions.ByLastName).Values.CopyTo(customers, 0);
            customerService.getCustomers(CustomerService.SortingOptions.ByLastName).Keys.CopyTo(customerIds, 0);
            int selectedCustomer = getSelectedOption(customers, "Who books a room?");

            selectedOption = 0;
            Console.Clear();
            if (hotelService.getHotels().Count == 0)
            {
                Console.WriteLine("There are no hotels in a database!");
                returnESC();
            }
            string[] hotels = new string[hotelService.getHotels().Count];
            int[] hotelIds = new int[hotelService.getHotels().Count];
            hotelService.getHotels().Values.CopyTo(hotels, 0);
            hotelService.getHotels().Keys.CopyTo(hotelIds, 0);
            int selectedHotel = getSelectedOption(hotels);
            Dictionary<int, int> roomsDict = hotelService.getRooms(hotelIds[selectedHotel]);

            selectedOption = 0;
            Console.Clear();
            if (roomsDict.Count == 0)
            {
                Console.WriteLine("There are no available rooms in " + hotels[selectedHotel]);
                returnESC();
            }
            int[] roomsNums = new int[roomsDict.Count];
            int[] roomsIds = new int[roomsDict.Count];
            roomsDict.Values.CopyTo(roomsNums, 0);
            roomsDict.Keys.CopyTo(roomsIds, 0);
            string[] str = roomsNums.Select(x => x.ToString()).ToArray();
            int selectedRoom = getSelectedOption(str, "Select a room:");

            bookingService.CreateBooking(customerIds[selectedCustomer], roomsIds[selectedRoom] );
            returnESC();
        }
        private void CancelBooking()
        {
            selectedOption = 0;
            Console.Clear();
            if (hotelService.getHotels().Count == 0)
            {
                Console.WriteLine("There are no hotels in a database!");
                returnESC();
            }
            string[] hotels = new string[hotelService.getHotels().Count];
            int[] hotelIds = new int[hotelService.getHotels().Count];
            hotelService.getHotels().Values.CopyTo(hotels, 0);
            hotelService.getHotels().Keys.CopyTo(hotelIds, 0);
            int selectedHotel = getSelectedOption(hotels, "Select a hotel:");

            selectedOption = 0;
            Console.Clear();
            var dict = bookingService.GetBookings(hotelIds[selectedHotel]);
            if (dict.Count == 0)
            {
                Console.WriteLine("No bookings!");
                returnESC();
            }
            string[] bookings = dict.Values.ToArray();
            int[] bookingIds = dict.Keys.ToArray();
            int selected = getSelectedOption(bookings, "Select a booking to cancel:");
            bookingService.RemoveBooking(bookingIds[selected]);

            returnESC();
        }

        private string getFromUser(string inputType)
        {
            string? input;
            Console.Write("Enter " + inputType + ":  ");
            input = Console.ReadLine();
            return input;
        }
        
        private void AddHotel()
        {
            Console.WriteLine("Adding a hotel to a database!");
            string name = getFromUser("hotel name");
            hotelService.AddHotel(name);
            Console.WriteLine("A new hotel was added!");
            returnESC();
        }
        private void RemoveHotel()
        {
            selectedOption = 0;
            Console.Clear();
            if (hotelService.getHotels().Count == 0)
            {
                Console.WriteLine("There are no hotels in a database!");
                returnESC();
            }
            string[] hotels = new string[hotelService.getHotels().Count];
            int[] hotelIds = new int[hotelService.getHotels().Count];
            hotelService.getHotels().Values.CopyTo(hotels, 0);
            hotelService.getHotels().Keys.CopyTo(hotelIds, 0);
            int selectedHotel = getSelectedOption(hotels, "Select a hotel to remove:");
            hotelService.RemoveHotel(hotelIds[selectedHotel]);

        }
        private void ShowHotels()
        {
            Console.Clear();
            if (hotelService.getHotels().Count == 0)
            {
                Console.WriteLine("There are no hotels in a database!");
                returnESC();
            }
            string[] hotels = new string[hotelService.getHotels().Count];
            int[] hotelIds = new int[hotelService.getHotels().Count];
            hotelService.getHotels().Values.CopyTo(hotels, 0);
            hotelService.getHotels().Keys.CopyTo(hotelIds, 0);

            int selected = getSelectedOption(hotels, "Select a hotel:");
            HotelMenu(hotelIds[selected], hotels[selected]);
        }
        private void HotelMenu(int hotelId, string hotelname)
        {
            Console.Clear();
            selectedOption = 0;
            int selected = getSelectedOption(hotelinfoOptions, hotelname);
            switch (selected)
            {
                case 0: AddRoom(hotelId); break;
                case 1: ShowRooms(hotelId, true); break;
                case 2: ShowRooms(hotelId, false); break;
                case 3: MainMenu(); break;
            }
        }
        private void AddRoom(int hotelId)
        {
            int number = Convert.ToInt32(getFromUser("room number"));
            decimal price = Convert.ToDecimal(getFromUser("price per day"));
            roomService.AddRoom(hotelId, number, price);
            Console.WriteLine($"Room number {number} was added!");
            returnESC();
        }
        private void ShowRooms(int hotelId, bool available)
        {
            Console.Clear ();
            Console.WriteLine(available? "Available rooms:": "Reserved rooms");
            foreach (var room in hotelService.getRooms(hotelId, available))
            {
                Console.WriteLine(room.Value);
            }
            returnESC();

        }
       
        private void displayOptions(string[] options, string title)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Length; i++)
            {
                string prefix;
                if (i == selectedOption)
                {
                    prefix = " > ";
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    prefix = "   ";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(prefix + options[i]);
            }
            Console.ResetColor();
        }
        private int getSelectedOption(string[] options, string title = "[ENTER] to select an option")
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                displayOptions(options, title);

                keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption == options.Length) selectedOption = 0;
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption == -1) selectedOption = options.Length - 1;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return selectedOption;
        }
        private void returnESC()
        {
            Console.WriteLine("\n   [ESC] to return");
            ConsoleKey keyPressed;
            do
            {
                keyPressed = Console.ReadKey(true).Key;
            } while (keyPressed != ConsoleKey.Escape);
            menuDelegate();
        }
    }
}
