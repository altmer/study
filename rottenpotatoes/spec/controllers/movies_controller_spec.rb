require 'spec_helper'

describe MoviesController do
  describe 'find with same director' do
    before :each do
      @movie1 = double('movie1', :director => 'director')
      @movie2 = double('movie2', :director => 'director')
      @movie3 = double('movie2', :director => '', :title => 'Alien')
      @fake_movies =  [@movie1, @movie2]      
    end
    
    it 'should retrieve movies with same director' do
      Movie.should_receive(:find).with('3').and_return( @movie1 )
      Movie.should_receive(:find_all_by_director).with('director').and_return(@fake_movies)
      assigns(:movies).should == @fake_results
      
      get :find_same_director, :id => 3
    end
    
    it 'should redirect to home page if movie does not have a director' do
      Movie.should_receive(:find).with('3').and_return( @movie3 )
      get :find_same_director, :id => 3
      response.should redirect_to movies_path      
    end

  end
end

