import { Injectable } from '@angular/core';
import { State, Action, StateContext, Selector } from '@ngxs/store';
import { PageTitleStateModel } from './page-title-state.model';
import { ChangePageTitle } from './page-title-actions';
import { Title } from '@angular/platform-browser';

const DEFAULT_TITLE = 'Elo-Baza';

@State<PageTitleStateModel>({
  name: 'pageTitle',
  defaults: {
    tabTitle: DEFAULT_TITLE,
    headerTitle: DEFAULT_TITLE,
    headerSubtitle: ''
  }
})
@Injectable()
export class PageTitleState {
  constructor(private bodyTitle: Title) {}

  @Selector()
  static tabTitle(state: PageTitleStateModel) {
    return state.tabTitle;
  }

  @Selector()
  static headerTitle(state: PageTitleStateModel) {
    return state.headerTitle;
  }

  @Selector()
  static headerSubtitle(state: PageTitleStateModel) {
    return state.headerSubtitle;
  }

  @Action(ChangePageTitle)
  changePageTitle(
    ctx: StateContext<PageTitleStateModel>,
    action: ChangePageTitle
  ) {
    const state = ctx.getState();

    const newTabTitle = action.tabTitle
      ? `${action.tabTitle} | ${DEFAULT_TITLE}`
      : DEFAULT_TITLE;

    this.bodyTitle.setTitle(newTabTitle);

    ctx.setState({
      ...state,
      tabTitle: newTabTitle,
      headerTitle: action.headerTitle ?? DEFAULT_TITLE,
      headerSubtitle: action.headerSubtitle
    });
  }
}
