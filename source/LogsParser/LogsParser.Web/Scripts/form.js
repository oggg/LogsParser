$(() => {
    $('#submitButton').on('click', (event) => {
        event.preventDefault();

        $('form').serializeArray()
            .map((v) => {
                localStorage.setItem(v.name, v.value);
            });

        location.href = location.href + 'Search/StringMatches'
    });
})