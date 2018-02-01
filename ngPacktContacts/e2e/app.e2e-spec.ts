import { NgPacktContactsPage } from './app.po';

describe('ng-packt-contacts App', () => {
  let page: NgPacktContactsPage;

  beforeEach(() => {
    page = new NgPacktContactsPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
