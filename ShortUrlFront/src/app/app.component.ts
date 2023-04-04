import { Component } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  baseUrl!: string;
  shortUrl!: string;
  errorMessage!: string;

  constructor(private http: HttpClient) { }

  onSubmit() {
    this.shortUrl = '';
    this.errorMessage = '';

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    this.http.post<string>('https://localhost:7191/url', { baseUrl: this.baseUrl }, { headers: headers, responseType: 'text' as 'json' })
      .subscribe(
        {
          next: (shortUrl: string) => this.shortUrl = shortUrl,
          error: error => {
            console.log('status code is not OK', error);
            this.errorMessage = error.error;
          }
        });
  }
}
