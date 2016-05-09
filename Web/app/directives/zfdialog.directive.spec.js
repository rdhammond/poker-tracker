describe('zfDialog', function () {
    var element, scope;

    beforeEach(module('app'));

    beforeEach(inject(function($rootScope, $compile) {
        var html = '<zf-dialog id="test-dialog"><p id="transclude-success"></p></zf-dialog>';
        scope = $rootScope.$new();
        element = $compile(html)(scope);
        scope.$digest();
    }));

    describe('#rootDiv', function () {
        it('adds reveal class', function () {
            expect(element.attr('class').indexOf('reveal')).not.toBeLessThan(0);
        });

        it('adds data-reveal', function () {
            expect(element.attr('data-reveal')).toBe('');
        })
    });

    describe('#contents', function () {
        it('transcludes custom content', function () {
            expect(element.find('p').attr('id')).toBe('transclude-success');
        });

        describe('closeButton', function () {
            it('has close button', function() {
                var button = element.find('button');
                expect(button.attr('class').indexOf('close-button')).not.toBeLessThan(0);
                expect(button.attr('data-close')).toBe('');
            });
        });
    });
});