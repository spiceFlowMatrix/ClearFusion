import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbfooterComponent } from './dbfooter.component';

describe('DbfooterComponent', () => {
  let component: DbfooterComponent;
  let fixture: ComponentFixture<DbfooterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbfooterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbfooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
