export class ChangePageTitle {
  static readonly type = '[PageTitle] Change Page Title';

  constructor(
    public tabTitle: string,
    public headerTitle: string,
    public headerSubtitle: string,
  ) {}
}
