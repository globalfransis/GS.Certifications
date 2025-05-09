const _persistentMessageKey = '__persistentMessage__'
const _persistentErrorMessageKey = '__persistentErrorMessage__'
var _skipPersistentMessageHandling = false;
class JourneyItem {

    constructor(key, url) {
        this.key = key;
        this.url = url;
    }
}

class Journey {

    _homeNavigationKey = __homeNavigationKey;
    _homeUrl = __homeUrl;

    constructor(navigationKey) {
        this.navigationKey = navigationKey
        this.loadJourney()
        if (_forceBack) { this.back(); }
        if (!_skipPersistentMessageHandling) {
            this.loadPersistentMessage()
        }
    }

    pushPage() {

        this.sliceJourney(this.navigationKey);

        if (this._journey[this._journey.length - 1].key !== this.navigationKey) {
            this._journey.push(new JourneyItem(this.navigationKey, location.href));
            this.saveJourney();
        }
        //Didn't work to manipulate the history:
        //var backPageIndex = Math.min(this._journey.length - 1, 1);  
        //history.pushState({}, this._journey[this._journey.length - backPageIndex - 1].url);
    }



    back(params) {

        var gone = this._journey.pop();

        var last = this._journey[this._journey.length - 1];

        while (last.key == gone.key) {
            gone = this._journey.pop();
        }

        if (params) {
            let operand = last.url.includes('?') ? '&' : '?'
            let newLastUrl = last.url + operand + params
            last.url = newLastUrl
        }

        this.saveJourney();

        location.href = last.url;

        return false;
    }

    navigate(url) {

        var gone = this._journey.pop();
        var last = this._journey[this._journey.length - 1];

        while (last.key == gone.key) {
            gone = this._journey.pop();
        }

        this.saveJourney();
        location.href = url;
        // window.location.replace(url);
        return false;
    }

    //private
    sliceJourney(cutFromThisPage) {

        var pageToCutIndex = this._journey.findIndex(item => item.key === cutFromThisPage);

        if (pageToCutIndex === -1) pageToCutIndex = this._journey.length;

        if (pageToCutIndex > 0) {
            this._journey = this._journey.slice(0, pageToCutIndex);
            this.saveJourney();
        }
        else if (pageToCutIndex == 0) {
            this._journey = [this._journey[0]];
            this.saveJourney();
        }
    }

    saveJourney() {
        sessionStorage.setItem('journey', JSON.stringify(this._journey));
    }

    loadJourney() {

        var journey = JSON.parse(sessionStorage.getItem('journey'));

        if (journey === null || journey.length === 0) {
            var homeJourney = [new JourneyItem(this._homeNavigationKey, this._homeUrl)];
            this._journey = homeJourney;
            this.saveJourney();
        }
        this._journey = journey;
    }

    loadPersistentMessage() {
        var message = sessionStorage.getItem(_persistentMessageKey)
        if (message?.length > 0 ?? false) {
            sessionStorage.removeItem(_persistentMessageKey);
            document.getElementById('clientPersistentMessage').innerHTML +=
                `<div class="text-center mt-4"><div class="col"><div class="fw-bold alert alert-success font-weight-bold">${message}</div></div></div >`;
        }
        var errormessage = sessionStorage.getItem(_persistentErrorMessageKey)
        if (errormessage?.length > 0 ?? false) {
            sessionStorage.removeItem(_persistentErrorMessageKey);
            document.getElementById('clientPersistentMessage').innerHTML +=
                `<div class="text-center mt-4"><div class="col"><div class="fw-bold alert alert-danger font-weight-bold">${errormessage}</div></div></div >`;
        }
    }

    getBackUrl() {
        return this._journey[this._journey.length - 1];
    }
}

var journey;

$(function () {
    journey = new Journey(_navigationKey, __homeNavigationKey);
    journey.pushPage();
});

function navigateBack() {
    //let journey = new Journey();
    //journey.back();
    navigateToPageWithoutMessage(baseUrl);
}

function navigateBackWithParams(params, message) {
    if (message) {
        sessionStorage.setItem(_persistentMessageKey, message)
    }
    _skipPersistentMessageHandling = true
    let journey = new Journey(false)
    
    journey.back(params);
}

function navigateBackWithMessage(message) {
    sessionStorage.setItem(_persistentMessageKey, message)
    _skipPersistentMessageHandling = true;
    var journey = new Journey(false);
    journey.back();
}
function navigateBackWithError(message) {
    sessionStorage.setItem(_persistentErrorMessageKey, message)
    _skipPersistentMessageHandling = true;
    var journey = new Journey(false);
    journey.back();
}

function navigateToPageWithError(url, message) {
    sessionStorage.setItem(_persistentErrorMessageKey, message)
    _skipPersistentMessageHandling = true;
    var journey = new Journey(false);
    journey.navigate(url);
}

function navigateToPage(url, message) {
    sessionStorage.setItem(_persistentMessageKey, message)
    _skipPersistentMessageHandling = true;
    var journey = new Journey(false);
    journey.navigate(url);
}

function navigateToPageWithoutMessage(url) {
    _skipPersistentMessageHandling = true;
    var journey = new Journey(false);
    journey.navigate(url);
}

function showConfirmation(message) {
    clearMessage();
    document.getElementById('clientPersistentMessage').innerHTML +=
        `<div class="text-center mt-4"><div class="col"><div class="fw-bold alert alert-success font-weight-bold">${message}</div></div></div >`;
}

function clearMessage() {
    document.getElementById('clientPersistentMessage').innerHTML = ''
}

function showError(errormessage) {
    clearMessage();
    document.getElementById('clientPersistentMessage').innerHTML +=
        `<div class="text-center mt-4"><div class="col"><div class="fw-bold alert alert-danger font-weight-bold">${errormessage}</div></div></div >`;
}

function clearMessage() {
    document.getElementById('clientPersistentMessage').innerHTML = ''
}