//Loading the DOM into jQuery
$(document).ready(function () {
    //here is where we put our code

    //select aynthing with the class of likes, and when it is clicked run a function 

    $('.likes').on('click', function () {
        //when we click, run dis code here
        //getting the ID from data-id in our likes div tag
        var id = $(this).data("id");
        //make a request to add a like
        var likesDiv = $(this);
        $.get("/Home/Like/" + id, function (data) {
            //replace the html that was clicked with the string returned from our get
            likesDiv.html(data);
        });
    });
    //adding a comment, bind a submit event to the addComment forms
    $('.addComment form').on('submit', function (event) {
        alert("working");
        //stsop the form from submitting normally,
        event.preventDefault();
        var theForm = $(this);
        //do the ajax post, use the serialize command to make a string of data
        $.post('/home/addComment', $(this).serialize(), function (data) {
            //display the content of data in an alert box
            theForm.parent().prepend(data);
        });
    });
});