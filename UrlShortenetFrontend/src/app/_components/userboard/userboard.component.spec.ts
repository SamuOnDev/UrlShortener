import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserBoardComponent } from './userboard.component';

describe('UserboardComponent', () => {
  let component: UserBoardComponent;
  let fixture: ComponentFixture<UserBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserBoardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
