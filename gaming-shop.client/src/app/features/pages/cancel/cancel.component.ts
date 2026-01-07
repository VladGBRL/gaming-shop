import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cancel',
  templateUrl: './cancel.component.html',
  styleUrl: './cancel.component.css'
})
export class CancelComponent {
  constructor(private router: Router) { }

  ngOnInit(): void {
    // Redirect to home page after 3 seconds
    setTimeout(() => {
      this.router.navigate(['/']);
    }, 3000);
  }
}
