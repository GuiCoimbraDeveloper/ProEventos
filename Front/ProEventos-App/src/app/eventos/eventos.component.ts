import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent {

  public eventos: any = [];
  public eventosFiltrados:any = [];
  exibirImagem:boolean=true;
  private _filtroLista:string = '';

  public get filtroLista(){
    return this._filtroLista;
  }
  public set filtroLista(value:string){
    this._filtroLista=value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista): this.eventos;
  }

  filtrarEventos(filtrarPor:string):any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento:any) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    ) ;
  }

  constructor(private http: HttpClient){ }

  ngOnInit():void{
    this.getEventos();
  }

  alterarImagem(){
    this.exibirImagem = !this.exibirImagem;
  }

  public getEventos(): void {
    this.http.get('https://localhost:7285/api/eventos').subscribe({
      next: (response) => {
        this.eventos=response
        this.eventosFiltrados=response
      },
      error: (e)=> console.log(e),
      complete: () => console.info('complete')
    });
  }
}
