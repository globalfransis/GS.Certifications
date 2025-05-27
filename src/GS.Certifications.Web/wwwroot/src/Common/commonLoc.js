const stringLocalizerList = {};

class Loc {

    constructor() {
    }

    loadResources(resourceGroup) {
        let groupsArray = [...resourceGroup, "sharedfront"];
        //each group is a resource file in the server
        //TODO: programatically get the resources for each group, this handlings can have 3 steps:
        //1. get the resources from localstorage
        //2. get the resources from js objects loaded in the page in the server side
        //3. get the resources from the server vía Ajax
        //Keep in mind you have to include a way to invalidate localStorage when the resources are updated, (maybe in the login page)?
    }

    /** Legacy method to get the resource from the page
     * @param {array} strings - The strings to be translated
     */
    loadLocList(strings) {
        let locArray = [];

        for (let i = 0; i < strings.length; i++) {
            locArray.push(window[strings[i]]);
        }

        for (let i = 0; i < locArray.length; i++) {
            Object.assign(stringLocalizerList, locArray[i], locArray[i + 1]);
        }
    }
    returnDefault(text) {
        console.warn(`"${text}" could not be translated.`)
        return text.toString()
    }

    getString(str) {
        return window.AppTexts.getString(str);
    }
}

/**
 * Proxy to handle non-translated strings, returns the original string
 * @param {string} text - The text to be translated
 */
let defaultLocValueHandler = {
    get: (target, text) => {
        return target.getString(text);
    },
};

let loc = new Loc();


export default loc = new Proxy(loc, defaultLocValueHandler)
