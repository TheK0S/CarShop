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


// Обработка нажатия кнопки
$('#updateCarListButton').click(function () {
    updateCarList();
});

// Функция для выполнения AJAX-запроса и обновления списка машин
function updateCarList() {
    // Собираем данные фильтрации
    var minPrice = $('.input-min').val();
    var maxPrice = $('.input-max').val();
    var selectedCategories = $('#carFilter input[type="checkbox"]:checked').map(function () {
        return $(this).val();
    }).get();
    var isFavourite = $('#carFilter input[type="checkbox"][name="flexSwitchCheck"]:checked').length > 0;

    // Выполняем AJAX-запрос
    $.ajax({
        type: 'POST', // или 'GET', в зависимости от вашего случая
        url: 'https://localhost:7279/cars/filter',
        data: {
            minPrice: minPrice,
            maxPrice: maxPrice,
            selectedCategories: selectedCategories,
            isFavourite: isFavourite
        },
        success: function (response) {
            // Обновляем список машин на странице
            $('#carField').html(response);
        },
        error: function (error) {
            console.log(error);
        }
    });
}
