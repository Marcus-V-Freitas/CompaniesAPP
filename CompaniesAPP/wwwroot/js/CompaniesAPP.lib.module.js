export function afterStarted(blazor) {
    Blazor.registerCustomEventType('custompaste', {
        browserEventName: 'paste',
        createEventArgs: event => {
            return {
                Value: event.clipboardData.getData('text')
            };
        }
    });
}