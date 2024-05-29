import { Component, Output, EventEmitter } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  standalone: true,
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
  imports: [
    MatIconModule
  ]
})
export class NavbarComponent {
  @Output() loginClick = new EventEmitter<void>();

  openLoginModal(): void {
    this.loginClick.emit();
  }
}
