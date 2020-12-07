# KManager

https://kmanagerwebmvc.azurewebsites.net/
 
Welcome to KManager! The all-in-one management application for K-Grocery stores across the nation.

Here is a user-friendly instruction manual for those wishing to use this application within the scope of their job:


First, make sure you create an account! You can do this by Registering.

Once you've registered, sign in!



After you have signed in, you'll be able to manage Item Classes, the Item Inventory, and the record of Orders!

To create an item:
-Provide the requested information. Making an item is probably the most straightforward part of the application, as there is nothing to pay too close attention too as it should otherwise be natural. Enter the description of the item as requested, and then create it. Editing the item class is similar.

To create an item in the inventory:
-Use the create feature and enter the corresponding item ID from the Item Classes. Be certain to use the correct ID, especially one within the system already!
-Items by default will not be marked as sold. To do this, edit the item using the Edit feature.

To create an order:
-This is the part of the process that requires precision. Orders expect a certain format:
A Comma-Separated-Values list. It will looked like: [1,3,5] (without the []). These numbers would correspond to item-classes 1, 3, and 5, assuming at least 5 item classes have been created and that each of those values, 1,3,5 exist within the database. Make sure to include the commas, but not anything other than the commas and numbers. No spaces!
The second part is another CSV. This time, it corresponds to the quantities of the items above. So if we had [30,10,5], this would refer to an order of 30 item #1s, 10 item #3s, and 5 item #5s.

Assuming that all goes as expected, you will see your order appear in the tabulation!
