describe('cardRooms', function () {
    var urlActions, $httpBackend, cardRooms;

    beforeEach(module('app'));

    beforeEach(inject(function (_$httpBackend_, _cardRooms_, _urlActions_) {
        $httpBackend = _$httpBackend_;
        cardRooms = _cardRooms_;
        urlActions = _urlActions_;
    }));

    describe('#get', function () {
        it('returns list from service', function () {
            var expected = [{ test: 1 }],
                checkResultsWrapper = { checkResults: checkResults },
                checkResultsSpy = spyOn(checkResultsWrapper, 'checkResults').and.callThrough();

            $httpBackend
                .expect('GET', urlActions.getCardRooms)
                .respond(200, expected);

            cardRooms.get().then(checkResultsWrapper.checkResults);
            $httpBackend.flush();
            $httpBackend.verifyNoOutstandingExpectation();
            expect(checkResultsWrapper.checkResults).toHaveBeenCalled();

            function checkResults(response) {
                expectDeepEqual(response, expected);
            }
        });
    });

    function expectDeepEqual(actual, expected) {
        return JSON.stringify(actual) === JSON.stringify(expected);
    }
});