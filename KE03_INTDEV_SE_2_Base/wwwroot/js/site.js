@section Scripts {
    <script>
        const searchBox = document.getElementById("searchBox");
        const suggestions = document.getElementById("suggestions");

        searchBox.addEventListener("input", function () {
        const value = this.value;

        // leeg → geen suggesties
        if (!value) {
            suggestions.innerHTML = "";
        return;
        }

        fetch(`/Products/SearchSuggestions?term=${encodeURIComponent(value)}`)
            .then(response => response.json())
            .then(data => {

            suggestions.innerHTML = "";

        if (data.length === 0) return;

                data.forEach(item => {
                    const li = document.createElement("li");
        li.className = "list-group-item list-group-item-action";
        li.style.cursor = "pointer";
        li.textContent = item;

        // klik = invullen + dropdown sluiten
        li.addEventListener("click", function () {
            searchBox.value = item;
        suggestions.innerHTML = "";
                    });

        suggestions.appendChild(li);
                });
            })
            .catch(err => {
            console.error("Autocomplete error:", err);
            });
    });

        // klik buiten dropdown → sluiten
        document.addEventListener("click", function (e) {
        if (!searchBox.contains(e.target) && !suggestions.contains(e.target)) {
            suggestions.innerHTML = "";
        }
    });
    </script>
}