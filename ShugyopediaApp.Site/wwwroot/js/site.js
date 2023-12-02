/*SEARCH TRAINING*/
document.getElementById('search-training').addEventListener('input', function () {
    var searchQuery = this.value.toLowerCase();
    document.querySelectorAll('.card').forEach(function (card) {
        var find_training = card.querySelector('.topic-word').textContent.toLowerCase();
        card.style.display = find_training.includes(searchQuery) ? 'block' : 'none';
    });
});

/*SEARCH CATEGORY*/
document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('search-category');
    const list = document.getElementById('category-list');
    const items = list.getElementsByTagName('li');

    searchInput.addEventListener('input', function () {
        const searchTerm = searchInput.value.toLowerCase();

        for (let i = 0; i < items.length; i++) {
            const text = items[i].innerText.toLowerCase();

            if (text.includes(searchTerm)) {
                items[i].style.display = 'block';
            } else {
                items[i].style.display = 'none';
            }
        }
    });
});




var sidebar = document.getElementById("sidebar");
var read_more = document.getElementById("read-more");

sidebar.addEventListener("mouseenter", () => {
    document.getElementById("nav-link").classList.remove("active");
})

sidebar.addEventListener("mouseleave", () => {
    document.getElementById("nav-link").classList.add("active");
})

read_more.addEventListener("onclick", () => {
    document.getElementById("nav-link").classList.add("active");
})