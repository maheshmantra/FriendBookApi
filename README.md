# FriendBookApi

Projected created with VisualStudio.

Basic features of the application should include:
	Users should be able to create a password-protected profile
	Users should be able to log in to their profile
	Users should be able to search other users and list the results
	Users should be able to add/remove other users as friends

Implementation:
1.	Token Based Authentication using ASP.NET Web API 2, Owin, and Identity.
2.	The API supports CORS and accepts HTTP calls from any origin.
3.	With the evolution of front-end frameworks and the huge change on how we build web applications nowadays the preferred approach to authenticate users is to use signed token as this token is sent to the server with each request
4.	We integrated swagger to test the API calls.


CRUD API - Functionality

Creating a password-protected profile:

        URL : http://{{URL}}/token
        Params :
        {  "userName": "william@gmail.com",  "password": "SuperPass",
          "confirmPassword": "SuperPass",
          "FirstName": "William",
          "LastName": "Smith",
          "Gender":"Male"}



Login to profile:

        URL : http://{{URL}}/token
        Form Data: 
        username:  “User name which was generated during registration process”
        Password: “Password created during registration process”
        grant_type: password



Viewing own profile info:

        URL : http://{{URL}}/get/User/{userName}
        
        

Searching other users and listing search results:

        URL: http://{{URL}}/get/User/FindFriends/{searchvalue}
        Note: SearchValue can be UserName or First Name or Last Name



Adding/Removing friends:

    Add friend
    
        URL : http://{{URL}}/User/MakeFriends
        Request Body:
        {
          "UserID": "4",
          "FriendIds": [1,2]
        }


    Remove Friend
    
        URL: http://{{URL}/User/unfriend
        Request Body:
         {
        "UserID":4,
        "FriendIds":[1,2]
        }

