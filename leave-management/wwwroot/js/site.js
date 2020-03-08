// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// from dataSets.net to make out tables resposive , we copy the script from th page and wrp it inside the section scripts{<script></script>
  //  then we take the id of the table and put it there, why we do that in index because we want index table to be resposive
  //here we will have the code one time on wwwroot/js/site.js, and use it everytime we have table only we nned to make sure we put id="tblData"     
$(document).ready(function () {
    $('#tblData').DataTable();
});