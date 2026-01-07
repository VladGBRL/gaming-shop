import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrl: './success.component.css'
})
export class SuccessComponent {
  constructor(private router: Router) { }

  ngOnInit(): void {
    // Redirect to home page after 3 seconds
    setTimeout(() => {
      this.router.navigate(['/']);
    }, 3000);
  }
}
