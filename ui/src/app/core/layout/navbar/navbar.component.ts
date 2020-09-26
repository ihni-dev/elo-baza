import { Component } from '@angular/core';
import { SECTIONS, Section } from './sections';

const SECTIONS_KEYS = Object.keys(SECTIONS);

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  get sections(): { [key: string]: Section } {
    return SECTIONS;
  }

  get sectionKeys(): string[] {
    return SECTIONS_KEYS;
  }
}
