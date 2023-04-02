# Add a declarative step here for populating the DB with movies.

Given /the following movies exist/ do |movies_table|
  movies_table.hashes.each do |movie|
    Movie.create!(movie)
  end
end

# Make sure that one string (regexp) occurs before or after another one
#   on the same page

Then /I should see "(.*)" before "(.*)"/ do |e1, e2|
  e1_found = false
  all(:xpath, "//table[@id='movies']/tbody/tr/td[1]/text()").each do |title|
     e1_found = (e1_found or (title.text == e1))
     assert ((title.text != e2) or (e1_found))
  end
end

Then /I should see all of the movies/ do
  if page.respond_to? :should
    page.should have_selector('table#movies tr', :count => 11)
  else
    assert page.has_selector?('table#movies tr', :count => 11)
  end
end

Then /the director of "(.*)" should be "(.*)"/ do |title, director|
  movie = Movie.find_by_title(title)
  assert movie.director == director
end


# Make it easier to express checking or unchecking several boxes at once
#  "When I uncheck the following ratings: PG, G, R"
#  "When I check the following ratings: G"

When /I (un)?check the following ratings: (.*)/ do |uncheck, rating_list|
  rating_list.split(/[,\s]+/).each do |rating| 
    step %{I #{uncheck}check "ratings[#{rating}]"}
  end
end
