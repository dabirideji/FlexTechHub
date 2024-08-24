// var apiBaseUrl = "http://localhost:5233";
// async function initiatePayment({ baseUrl = apiBaseUrl, amount, currency = "NGN", redirectUrl, paymentOption = "card", paymentGateway, customerEmail, customerPhoneNumber = '', customerName = '' }) {
//     const paymentRequest = {
//         amount: parseFloat(amount),
//         currency: currency,
//         redirectUrl: redirectUrl,
//         paymentOption: paymentOption,
//         customerEmail: customerEmail,
//         customerPhoneNumber: customerPhoneNumber || '', // Ensure it's an empty string if not provided
//         customerName: customerName || '' // Ensure it's an empty string if not provided
//     };

//     try {
//         let apiUrl;
//         let paymentLinkKey;

//         if (paymentGateway === "flutterwave") {
//             apiUrl = `${baseUrl}/create-flutterwave-payment`;
//             paymentLinkKey = "link";
//         } else if (paymentGateway === "paystack") {
//             apiUrl = `${baseUrl}/create-paystack-payment`;
//             paymentLinkKey = "authorization_url";
//         }

//         const response = await fetch(apiUrl, {
//             method: "POST",
//             headers: {
//                 "Content-Type": "application/json"
//             },
//             body: JSON.stringify(paymentRequest)
//         });

//         const data = await response.json();
//         const parsedData = typeof data === "string" ? JSON.parse(data) : data;

//         // Access the payment link based on the selected gateway
//         const paymentLink = parsedData.data[paymentLinkKey];
//         console.log(paymentLink);

//         // Redirect the user to the payment link
//         window.location.href = paymentLink;
//     } catch (error) {
//         console.error("Error:", error);
//     }
// }




//     function makePayment(platform,amount,redirectUrl="#",customerEmail,customerName=null,customerPhoneNumber=null){
//         initiatePayment({
//             amount,
//             currency: "NGN",
//             redirectUrl,
//             paymentGateway: platform,
//             customerEmail,
//             customerPhoneNumber,
//             customerName
//         });
//     }


//     function makeFlutterwavePayment(amount,redirectUrl=null,customerEmail,customerName=null,customerPhoneNumber=null)
//     {
//         makePayment("flutterwave",amount,redirectUrl,customerEmail,customerName,customerPhoneNumber);
//     }

//     function makePaystackPayment(amount,redirectUrl=null,customerEmail,customerName=null,customerPhoneNumber=null)
//     {
//         makePayment("paystack",amount,redirectUrl,customerEmail,customerName,customerPhoneNumber);
//     }


























// var apiBaseUrl = "http://localhost:5233";
var apiBaseUrl = "";

async function initiatePayment({ baseUrl = apiBaseUrl, amount, currency = "NGN", redirectUrl, paymentOption = "card", paymentGateway, customerEmail, customerPhoneNumber = '', customerName = '' }) {
    const paymentRequest = {
        amount: parseFloat(amount),
        currency: currency,
        redirectUrl: redirectUrl,
        paymentOption: paymentOption,
        customerEmail: customerEmail,
        customerPhoneNumber: customerPhoneNumber || '', // Ensure it's an empty string if not provided
        customerName: customerName || '' // Ensure it's an empty string if not provided
    };

    try {
        let apiUrl;
        let paymentLinkKey;

        if (paymentGateway === "flutterwave") {
            // apiUrl = `${baseUrl}/api/create-flutterwave-payment`;
            apiUrl = `/api/payment/create-flutterwave-payment`;
            paymentLinkKey = "link";
        } else if (paymentGateway === "paystack") {
            // apiUrl = `${baseUrl}/api/create-paystack-payment`;
            apiUrl = `/api/payment/create-paystack-payment`;
            paymentLinkKey = "authorization_url";
        } else {
            throw new Error("Unsupported payment gateway");
        }

        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(paymentRequest)
        });

        const data = await response.json();
        
        if (!data.Status) {
            throw new Error(data.ResponseMessage);
        }

        const paymentLink = data.Data.data[paymentLinkKey];
        if (!paymentLink) {
            throw new Error("Payment link not found in response");
        }

        // Redirect the user to the payment link
        window.location.href = paymentLink;
    } catch (error) {
        console.error("Error:", error.message);
        // Handle the error according to your requirements
    }
}

function makePayment(platform, amount, redirectUrl = "#", customerEmail, customerName = null, customerPhoneNumber = null) {
    initiatePayment({
        amount,
        currency: "NGN",
        redirectUrl,
        paymentGateway: platform,
        customerEmail,
        customerPhoneNumber,
        customerName
    });
}

function makeFlutterwavePayment(amount, redirectUrl = null, customerEmail, customerName = null, customerPhoneNumber = null) {
    makePayment("flutterwave", amount, redirectUrl, customerEmail, customerName, customerPhoneNumber);
}

function makePaystackPayment(amount, redirectUrl = null, customerEmail, customerName = null, customerPhoneNumber = null) {
    makePayment("paystack", amount, redirectUrl, customerEmail, customerName, customerPhoneNumber);
}





document.getElementById('signupForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Prevent the default form submission

    // Get form values
    const name = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const courseSelect = document.getElementById('course');
    const selectedOption = courseSelect.options[courseSelect.selectedIndex];
    const coursePrice = selectedOption.getAttribute('data-price');

    // Check if a course is selected
    if (!coursePrice) {
        alert('Please select a course.');
        return;
    }

    // Call the payment function
    makePaystackPayment(
        coursePrice,        // Amount from selected course
        "/",                // Redirect URL (update if needed)
        email,              // Customer email
        name,               // Customer name
        ''                  // Customer phone number (update if needed)
    );
});