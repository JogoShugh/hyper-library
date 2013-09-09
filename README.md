hyper-library
=============

A reference Hypermedia API 

The requirements below cover CRUD management of a library as well as a library book checkout/check in workflow.

1 Scenario: Retrieving books
  * Given existing books
    * When a GET request is made for all books
      * Then all books are returned
          
  * Given an existing books
     * When a GET request is made for it
      * Then it is returned
      * Then it should have an id
      * Then it should have a title
      * Then it should have a description
      * Then it should have a state(check in,checked out )
      * Then it should have a self link
      * Then it should have a transition links
  * Given an existing checked in book
    * When a GET request is made for it
      * Then it should have a checkout link
  * Given an existing checked out book
    * When a GET request is made for it
      * Then it should have a check in link
  * Given a book does not exist
    * When a GET request is made for it
      * Then a '404 Not Found' status is returned
      
2 Scenario: Creating an book
   * Given a newly created book
     * When a valid POST request is made
        * Then an book resource should be created
        * Then it should be returned
        * Then a '201 Created' status is returned
        * Then the location header will be set on the reponse to the resource location
            
3 Scenario: Deleting book
  * Give an existing book
    * When a DELETE request is made for it
      * Then it should be deleted
       * Then a '200 OK' status is returned
  * Given a book does not exist
    * When a DELETE request is made for it
      * Then a '404 Not Found' status is returned
    
4 Scenario: Updating a book
  * Given an existing book
    * When a PATCH request is made for it
        * Then the book resource is updated
        * Then a '200 OK' is returned
  * Given an issue does not exist
   * When a PATCH request is made for it
        * Then a '404 Not Found' status is returned
    
5 Scenario: Checking out a book
  * Given an existing checked in book is to be checked out
    * When a POST request is made with a checkout action
        * Then it is checked out
        * Then a '200 OK' is returned
  *Given an existing checked out book is to be placed on hold 
    * When a POST request is made with a checkin action
        * Then it is checked in
        * Then a '200 OK' is returned
        * 
6 Scenario: Checking in a book
  * Given an existing checked out book is to be checked in
    * When a POST request is made with a checkout action
        * Then it is checked in
        * Then a '200 OK' is returned
  *Given an existing checked in book is not able to be checked back in
    * When a POST request is made with a checkout action
        * Then a '403 forbidden' is returned
