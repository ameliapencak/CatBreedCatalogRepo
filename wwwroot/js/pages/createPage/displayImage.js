document.addEventListener('DOMContentLoaded', function () {
    var imageInput = document.getElementById('image');
    if (imageInput) {
        imageInput.addEventListener('change', function (event) {
            var output = document.getElementById('preview-image');
            output.src = URL.createObjectURL(event.target.files[0]);
            output.onload = function () {
                URL.revokeObjectURL(output.src) // Oczyszcza pamięci po załadowaniu zdjecia
            };
            output.style.display = 'block'; // Pokazuje zdjecie, gdy plik zostanie wybrany
        });
    }
});
