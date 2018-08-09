$(function () {
    let folder = localStorage.getItem('FolderSelector');
    let searchPattern = localStorage.getItem('SearchPattern');

    if (folder && searchPattern) {
        let matchCounter = 0;
        let matchHubProxy = $.connection.matchHub;
        matchHubProxy.client.addLinkForMatch = function (matchModel) {
            matchCounter++;
            let resultCounter = new Array(3 - matchCounter.toString().length).join(0);
            resultCounter = resultCounter + matchCounter
            let link = `<p>
                                <a href="/search/getpid?path=${matchModel.Path}&row=${matchModel.Row}&col=${matchModel.Col}">
                                    ${matchModel.Filename}_Match_${resultCounter}
                                </a>
                            </p>`;
            $('#match-container').append(link);
        }

        $.connection.hub.start().done(function () {
            matchHubProxy.server.findMatch(folder, searchPattern);
        });
    }
});