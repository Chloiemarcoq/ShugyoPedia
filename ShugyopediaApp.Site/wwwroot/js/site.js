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