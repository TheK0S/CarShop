const rangeInput = document.querySelectorAll(".range-input input"),
    priceInput = document.querySelectorAll(".price-input input"),
    range = document.querySelector(".slider .progress");
let priceGap = 1000;
priceInput.forEach(input => {
    input.addEventListener("input", e => {
        let minPrice = parseInt(priceInput[0].value),
            maxPrice = parseInt(priceInput[1].value);

        if ((maxPrice - minPrice >= priceGap) && maxPrice <= rangeInput[1].max) {
            if (e.target.className === "input-min") {
                rangeInput[0].value = minPrice;
                range.style.left = ((minPrice / rangeInput[0].max) * 100) + "%";
            } else {
                rangeInput[1].value = maxPrice;
                range.style.right = 100 - (maxPrice / rangeInput[1].max) * 100 + "%";
            }
        }
    });
});
rangeInput.forEach(input => {
    input.addEventListener("input", e => {
        let minVal = parseInt(rangeInput[0].value),
            maxVal = parseInt(rangeInput[1].value);
        if ((maxVal - minVal) < priceGap) {
            if (e.target.className === "range-min") {
                rangeInput[0].value = maxVal - priceGap
            } else {
                rangeInput[1].value = minVal + priceGap;
            }
        } else {
            priceInput[0].value = minVal;
            priceInput[1].value = maxVal;
            range.style.left = ((minVal / rangeInput[0].max) * 100) + "%";
            range.style.right = 100 - (maxVal / rangeInput[1].max) * 100 + "%";
        }
    });
});




const priceRange = "100-500";
const categories = ["sedan", "suv"];

// Формируйте объект параметров запроса
const queryParams = {
    priceRange: priceRange,
    category: categories.join(",") // Преобразуйте массив категорий в строку
};

// Преобразуйте объект параметров в строку запроса
const queryString = new URLSearchParams(queryParams).toString();

// Задайте URL вашего API
const apiUrl = "https://your-api-url.com/api/cars"; // Замените на фактический URL вашего API

// Формируйте URL с параметрами запроса
const urlWithParams = `${apiUrl}?${queryString}`;

// Выполните GET-запрос с использованием fetch
fetch(urlWithParams)
    .then(response => {
        if (response.ok) {
            return response.json(); // Распарсить JSON из ответа
        } else {
            throw new Error(`Ошибка: ${response.status} - ${response.statusText}`);
        }
    })
    .then(data => {
        console.log("Ответ от сервера:", data);
    })
    .catch(error => {
        console.error("Произошла ошибка:", error);
    });