import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { Badge } from './models/badge';

@Component({
  selector: 'app-badges-card',
  templateUrl: './badges-card.component.html',
  styleUrls: ['./badges-card.component.css']
})

export class BadgesCardComponent implements OnInit {
  
  badges$: Observable<Badge[]> = new Observable;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    {
      this.badges$ = this.httpClient.get<Badge[]>("../../assets/badges.json");
    }
  }

}
