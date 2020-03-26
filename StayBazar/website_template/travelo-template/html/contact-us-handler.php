<?php 
$myemail = 'soaptheme@gmail.com';//<-----Put Your email address here.

if ( empty($_POST['name']) || 
    empty($_POST['email']) || 
    empty($_POST['message'] )) {
    echo "Error: Name, Email and Message fields are required";
    exit;
}

$name = $_POST['name'];
$email_address = $_POST['email'];
$subject = isset($_POST['subject']) ? $_POST['subject'] : "";
$message = $_POST['message'];
$website = isset($_POST['website']) ? $_POST['website'] : "";

if ( !preg_match(
    "/^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$/i", 
    $email_address ) ) {
    echo "Invalid email address";
    exit;
}

$to = $myemail; 
$email_subject = "Contact form submission" . ( empty($subject) ? "" : ": " . $subject );
$email_body = "You have received a new message. ".
" Here are the details:\n\nName: $name \nEmail: $email_address " . ( empty($website) ? "" : "\nWebsite: " . $website ) . "\nMessage: \n\n$message"; 

$headers = "From: $myemail\n"; 
$headers .= "Reply-To: $email_address";

if ( mail( $to, $email_subject, $email_body, $headers ) ) {
    echo "Successfully sent!";
} else {
    echo "Please check your mail server!";
}

exit;
