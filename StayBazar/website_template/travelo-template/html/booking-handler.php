<?php 
$myemail = 'soaptheme@gmail.com';//<-----Put Your email address here.

if ( empty( $_POST['firstname'] ) || empty( $_POST['lastname'] ) || empty( $_POST['email'] ) ||
    empty( $_POST['vemail'] ) || empty( $_POST['country_code'] ) || empty( $_POST['phone'] ) ) {
    echo "All personal information fields are required!";
    exit;
}

$firstname = $_POST['firstname'];
$lastname = $_POST['lastname'];
$email_address = $_POST['email'];
$vemail_address = $_POST['vemail'];
$country_code = $_POST['country_code'];
$phone = $_POST['phone'];

if ( !preg_match(
    "/^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$/i", 
    $email_address ) ) {
    echo "Invalid e-mail address";
    exit;
}

if ( $email_address !== $vemail_address ) {
    echo "Please input verification e-mail address correctly.";
    exit;
}

$to = $myemail; 
$email_subject = "Booking form submission" . ( empty($subject) ? "" : ": " . $subject );
$email_body = "You have received a new message. ".
" Here are the details:\n\nName: $lastname $firstname \nEmail: $email_address\nCountry Code: country_code\nPhone number: $phone\nReceive Travelo promotional offer: " . ( empty( $_POST['promo_offer'] ) ? "No" : "Yes" );

$headers = "From: $myemail\n"; 
$headers .= "Reply-To: $email_address";

if ( mail( $to, $email_subject, $email_body, $headers ) ) {
    echo "Successfully sent!";
} else {
    echo "Please check your mail server!";
}

exit;