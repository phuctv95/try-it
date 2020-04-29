const { Builder, By } = require('selenium-webdriver');
const chrome = require('selenium-webdriver/chrome');

const dcn = {
    url: 'https://dotchuoinon.com/'
};

async function webElesToStrings(tags) {
    let result = [];
    for (let i = 0; i < tags.length; i++ ) {
        result.push(await tags[i].getText());
    }
    return result;
}

(async () => {
    let options = new chrome.Options()
    options.addArguments('--headless');
    let driver = await new Builder().forBrowser('chrome').setChromeOptions(options).build();
    
    // Go to Dcn.
    await driver.get(dcn.url);
    console.log(`Current url: ${await driver.getCurrentUrl()}`);

    // Get post list.
    let posts = [];
    let articles = await driver.findElements(By.xpath('//article'));
    for (let i = 0; i < articles.length; i++) {
        let article = articles[i];
        let tagEles = await article.findElements(By.xpath('./footer//a'));
        posts.push({
            title: await article.findElement(By.xpath('./header/h1')).getText(),
            datetime: await article.findElement(By.xpath('./header//span[@class="entry-date"]')).getText(),
            author: await article.findElement(By.xpath('./header//span[@class="byline"]')).getText(),
            tags: await webElesToStrings(tagEles),
            url: await article.findElement(By.xpath('./header/h1/a')).getAttribute('href')
        });
        console.log(`#${i + 1}`);
        console.log(`  - Title: ${posts[i].title}`);
        console.log(`  - Time: ${posts[i].datetime}`);
        console.log(`  - Author: ${posts[i].author}`);
        console.log(`  - Tags: ${posts[i].tags}`);
        console.log(`  - Url: ${posts[i].url}`);
    }

    driver.quit();
})();
