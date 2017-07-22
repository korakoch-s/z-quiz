import { ZQuiz.NgAppPage } from './app.po';

describe('z-quiz.ng-app App', () => {
  let page: ZQuiz.NgAppPage;

  beforeEach(() => {
    page = new ZQuiz.NgAppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
