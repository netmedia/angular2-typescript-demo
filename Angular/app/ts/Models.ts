export class Article {
  Id:     number;
  Votes:  number;
  Title:  string;
  Link:   string;

  constructor(title: string, link: string, votes?: number, id?: number) {
    this.Title  = title;
    this.Link   = link;
    this.Votes  = votes || 0;
    this.Id     = id || 0;
  }

  voteUp(): void {
    this.Votes += 1;
  }

  voteDown(): void {
    this.Votes -= 1;
  }

  domain(): string {
    try {
      const link: string = this.Link.split('//')[1];
      return link.split('/')[0];
    } catch (err) {
      return null;
    }
  }
}