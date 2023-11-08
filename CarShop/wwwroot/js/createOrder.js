const deliveryCity = document.getElementById('deliveryCity'),
    deliveryAddress = document.getElementById('deliveryAddress'),
    paymentMethod = document.getElementById('paymentMethod'),
    post = document.getElementById('post')

const novaPost = getNovaPost();
const ukrPost = getUkrPost();


post.addEventListener('change', function () {
    deliveryCity.innerHTML = '';

    if (post.value === 'novaPost') {
        for (key in novaPost.departments) {
            const option = document.createElement('option');
            option.text = key;
            option.value = key;
            deliveryCity.appendChild(option);
        }
    } else if (post.value === 'ukrPost') {
        for (key in ukrPost.departments) {
            const option = document.createElement('option');
            option.text = key;
            option.value = key;
            deliveryCity.appendChild(option);
        }
    }
});

deliveryCity.addEventListener('change', function () {
    deliveryAddress.innerHTML = '';
    const selectedPost = post.value === 'novaPost' ? novaPost : ukrPost;

    selectedPost.departments[deliveryCity.value].forEach(value => {
        const option = document.createElement('option');
        option.text = value;
        option.value = value;
        deliveryAddress.appendChild(option);
    });
});



    async function getNovaPost() {
        try {
            const response = await fetch('https://localhost:7279/novapost');

            if (!response.ok) {
                throw new Error('Error' + response.status);
            }
            return await response.json();

        } catch (error) {
            console.log(error);
        }
    }

async function getUkrPost() {
    try {
        const response = await fetch('https://localhost:7279/ukrpost');

        if (!response.ok) {
            throw new Error('Error' + response.status);
        }
        return await response.json();

    } catch (error) {
        console.log(error);
    }
}