import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent {

  public eventos: any;

  constructor(private http: HttpClient){ }

  ngOnInit():void{
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:7285/api/eventos').subscribe({
      next: (response) => this.eventos=response,
      error: (e)=> console.log(e),
      complete: () => console.info('complete')
    });
  }
}
