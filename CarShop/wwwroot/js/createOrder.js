const deliveryCity = document.getElementById('deliveryCity')
const deliveryAddress = document.getElementById('deliveryAddress')
const paymentMethod = document.getElementById('paymentMethod')
const post = document.getElementById('post')
let novaPost = {};
let ukrPost = {};


setDeliveryCityOptions();


(async () => {
    await getNovaPost();
    await getUkrPost();

    console.log(novaPost)
    console.log(ukrPost)

    setDeliveryCityOptions();
    post.addEventListener('change', setDeliveryCityOptions);
    deliveryCity.addEventListener('change', setDeliveryAddressOption);
})();




function setDeliveryCityOptions() {
    deliveryCity.innerHTML = '<option></option>';
    deliveryAddress.innerHTML = '';

    if (post.value === 'Nova Post') {
        for (key in novaPost.Departments) {
            const option = document.createElement('option');
            option.text = key;
            option.value = key;
            deliveryCity.appendChild(option);
        }
    } else if (post.value === 'Ukr Post') {
        for (key in ukrPost.Departments) {
            const option = document.createElement('option');
            option.text = key;
            option.value = key;
            deliveryCity.appendChild(option);
        }
    }
}


function setDeliveryAddressOption() {
    deliveryAddress.innerHTML = '';
    const selectedPost = post.value === 'Nova Post' ? novaPost : ukrPost;

    selectedPost.Departments[deliveryCity.value].forEach(value => {
        const option = document.createElement('option');
        option.text = value;
        option.value = value;
        deliveryAddress.appendChild(option);
    });
}


async function getNovaPost() {
    try {
        const response = await fetch('https://localhost:7279/novapost');
        const data = await response.json();
        novaPost = data;
    } catch (error) {
        console.error(error);
    }
}


async function getUkrPost() {
    try {
        const response = await fetch('https://localhost:7279/ukrpost');
        const data = await response.json();
        ukrPost = data;
    } catch (error) {
        console.error(error);
    }
}