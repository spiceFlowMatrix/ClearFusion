import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayoutMinutesComponent } from './playout-minutes.component';

describe('PlayoutMinutesComponent', () => {
  let component: PlayoutMinutesComponent;
  let fixture: ComponentFixture<PlayoutMinutesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayoutMinutesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayoutMinutesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
