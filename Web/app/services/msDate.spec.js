describe('msDate', function () {
    var msDate;

    beforeEach(module('app'));

    beforeEach(inject(function (_msDate_) {
        msDate = _msDate_;
    }));

    describe('#from', function () {
        it('converts from javascript', function () {
            var now = new Date(),
                actual = msDate.from(now);

            expect(actual).toBe('/Date(' + now.getTime() + ')/');
        });
    });
});