var currentIndex = 0;
var items = document.querySelectorAll('.carousel-content > div');

function showItem(index) {
    currentIndex = index;
    items.forEach(item => item.style.display = 'none');
    items[index].style.display = 'flex';
}

setInterval(function () {
    currentIndex = (currentIndex + 1) % items.length;
    showItem(currentIndex);
}, 2000);

showItem(currentIndex);